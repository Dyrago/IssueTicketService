import type { LoginDto } from '$lib/types/Login/LoginDto';
import type { Actions } from './$types';
import { fail, redirect } from '@sveltejs/kit';

export const actions = {
	default: async ({request, fetch, cookies}) => {
		const data = await request.formData();
        const email = data.get("email");
        const password = data.get("password");

        if (!email || !password) {
			return fail(400, { missing: true });
		}

        try{
            const response = await fetch("http://api:8080/Authentication/Login", {
                method: "POST",
                headers: {
                    "Content-Type" : "application/json"
                },
                body: JSON.stringify({email, password})
            });

            if(!response.ok)
            {
                const errorData = await response.json().catch(() => ({}));
                return fail(response.status, { 
					incorrect: true, 
					message: errorData.message || 'Błędne dane logowania' 
				});
            }

            const result = await response.json();

            if (result.data)
            {
                cookies.set('authJwt', result.authToken, {
					path: '/',
					httpOnly: true,
					sameSite: 'strict',
					maxAge: 60 * 5
				});
                cookies.set('RfsTkn', result.token, {
					path: '/',
					httpOnly: true,
					sameSite: 'strict',
					maxAge: 60 * 60 * 24 * 7 
				});
            }
        }
        catch (error)
        {
            return fail(500, { error: 'Błąd połączenia z serwerem' });
        }

        throw redirect(303, '/Login');
	}
} satisfies Actions;