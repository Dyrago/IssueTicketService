import type { Cookies } from '@sveltejs/kit';
import type { Tokens } from '$lib/types/Login/Tokens';

export const setAuthCookies = (cookies: Cookies, tokens: Tokens) => {
  const opts = { path: '/', httpOnly: true, sameSite: 'strict' as const};
  cookies.set('authJwt', tokens.authToken, { ...opts, maxAge: 60 * 5 });          // = Jwt__ExpirationInMinutes
  cookies.set('RfsTkn', tokens.refreshToken, { ...opts, maxAge: 60 * 60 * 24 * 7 }); // = ważność refresh tokenu
}

export const clearAuthCookies = (cookies: Cookies) => {
  cookies.delete('authJwt', { path: '/' });
  cookies.delete('RfsTkn', { path: '/' });
}