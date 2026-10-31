import { env } from '$env/dynamic/private';
import type { Tokens } from '$lib/types/Login/Tokens';

export type RefreshResult = {tokens?: Tokens, rejected?: boolean}

//const inflight = new Map<string, Promise<RefreshResult>>();


export const callRefresh = async(refreshToken: string): Promise<RefreshResult> => {
    let result: Response;
    try
    {
        result = await fetch(`${env.INTERNAL_API_URL}/Authentication/Refresh`, {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(refreshToken),
        signal: AbortSignal.timeout(5000)
        });
    }
    catch (e: any)
    {
        console.error("Can not refresh access.", e);
        return {};
    }

    console.log('[refresh] status', result.status);
    if(!result?.ok) return { rejected: [400,401,403].includes(result.status) };


    const data = (await result.json().catch(() => null));
    if (data?.authToken && data?.refreshToken){
        const tokens: Tokens =
        {
            authToken: data?.authToken,
            refreshToken: data?.refreshToken
        }

        return {tokens: tokens}
    }
    
        
    return {rejected: true}   
}

// export const refreshOnce = async (refreshToken: string): Promise<RefreshResult> => {

//     let pendingRefresh = inflight.get(refreshToken);

//     if (pendingRefresh){
//         return pendingRefresh;
//     }

//     const pending = callRefresh(refreshToken);
//     inflight.set(refreshToken, pending);
//     setTimeout(() => inflight.delete(refreshToken), 10_000);
//     return pending;
//   }