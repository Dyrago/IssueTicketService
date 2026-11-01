import type { RequestHandler } from '@sveltejs/kit'; 
import { error } from '@sveltejs/kit';
import { env } from '$env/dynamic/private'

export const POST: RequestHandler = async ({ cookies, fetch }) => {
    try {
        const refreshCookie = cookies.get('RfsTkn');
        const authCookie = cookies.get('authJwt');

        const response = await fetch(`${env.INTERNAL_API_URL}/Authentication/Logout`, {
            method: 'POST',
            headers: { 
                'Content-Type': 'application/json',
                'cookie': `authJwt=${authCookie}; RfsTkn=${refreshCookie}`
            }
        });

        return new Response(response.body, {
            status: response.status,
            headers: response.headers
        });

    } catch (err) {
        throw error(500, 'Błąd komunikacji z wewnętrznym API');
    }
};