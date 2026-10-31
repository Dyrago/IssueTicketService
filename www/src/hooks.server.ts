import { callRefresh } from "$lib/api/refresh";
import { setAuthCookies, clearAuthCookies } from "$lib/api/auth";
import type { Handle, HandleFetch } from '@sveltejs/kit';
import { redirect } from '@sveltejs/kit';
import { sequence } from '@sveltejs/kit/hooks';
import { env } from '$env/dynamic/private';

const refreshAccess: Handle = async ({ event, resolve }) => {

  let authToken = event.cookies.get('authJwt') ?? null;
  let refreshToken = event.cookies.get('RfsTkn') ?? null;

  if (!authToken && refreshToken) {
    const { tokens, rejected } = await callRefresh(refreshToken);


    if (tokens) setAuthCookies(event.cookies, tokens);
    else if (rejected) clearAuthCookies(event.cookies);
  }

  return resolve(event);
};

const guard: Handle = async ({ event, resolve }) => {
  if (!event.cookies.get('authJwt') && !event.url.pathname.startsWith('/Login')) {
    redirect(303, '/Login');
  }
  return resolve(event);
};

export const handle = sequence(refreshAccess, guard);

export const handleFetch: HandleFetch = ({ event, request, fetch }) => {
  let accessToken = event.cookies.get("authJwt")

  if (accessToken && request.url.startsWith(env.INTERNAL_API_URL!)) {
    request.headers.set('authorization', `Bearer ${accessToken}`);
  }
  return fetch(request);
};