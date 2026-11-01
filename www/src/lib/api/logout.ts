import { goto, invalidateAll } from "$app/navigation";

export const logout = async () => {

    const response = await fetch('api/auth/logout',{
        method: "POST",
    })

    if (!response?.ok) {
        throw new Error('Wylogowanie nie powiodło się');
    }

    await invalidateAll();

    await goto("/Login");
}