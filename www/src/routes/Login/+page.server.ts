import type { Actions, PageServerLoad } from './$types';
import { fail, redirect } from '@sveltejs/kit';
import { setAuthCookies, clearAuthCookies } from '../../lib/api/auth';
import { logIn } from '$lib/api/login';

export const load: PageServerLoad = ({ cookies }) => {
  if (cookies.get('authJwt')) redirect(303, '/Dashboard');
};

export const actions = {
	default: async ({request, cookies}) => {

		const data = await request.formData();
        const result = await logIn(data);

        if(!result.ok){
            return fail(result.status, {message: result.message, email: data.get('email')?.toString() ?? ''})
        }

        setAuthCookies(cookies, result.tokens);
        redirect(303, '/Dashboard')  
	}
} satisfies Actions;