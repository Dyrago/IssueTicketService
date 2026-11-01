import type { LayoutServerLoad } from './$types';
import { env } from "$env/dynamic/private"
export const load : LayoutServerLoad = async({cookies, fetch}) => {

    try
    {
        let userAuth = cookies.get("authJwt");

        let response;

        if(userAuth)
        {
            response = await fetch(`${env.INTERNAL_API_URL}/Authentication/Current`, {method: "GET"});
        }
        
        if(!response?.ok)
            return {user: null};
        
        return {user: await response.json().catch(() => null) } 
    }
    catch
    {
        return {user: null};
    }
}