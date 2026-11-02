import { fail, error, redirect, json } from '@sveltejs/kit';
import { env } from '$env/dynamic/private'
import type { TicketsSearchFilters } from '$lib/types/Tickets/TicketsSearchFilters';

import type { PageServerLoad, Actions } from './$types';
import { fromAction } from 'svelte/attachments';

export const load : PageServerLoad = async({fetch}) =>
{
    try
    {
        const response = await fetch(`${env.INTERNAL_API_URL}/Tickets`, {
            method:"POST",
            headers:{
                "Content-Type":"application/json"
            },
            body: JSON.stringify({})
        });

        if(!response?.ok)
            return {tickets: []};

        const tickets = await response.json();
        return {tickets: tickets};
    }
    catch(error)
    {
        console.error("Błąd podczas wykonania zapytania", error)
        return {tickets: null};
    }
}

export const actions: Actions ={
    default: async({request, fetch}) => {

        let formFilters = await request.formData();

        const searchFilters : TicketsSearchFilters = {
            Contains: null,
            DateFrom: null,
            DateTo: null,
            ReporterId: null,
            ResolverId: null,
            TicketStatusId: null
        }

        try{

            const response = await fetch(`${env.INTERNAL_API_URL}/Tickets`, {
                method:"POST",
                headers:{
                    "Content-Type":"application/json"
                },
                body: JSON.stringify(searchFilters)
            });

            if(!response?.ok)
                return {tickets: []};

            const tickets = await response.json();

            return{tickets: tickets};
            
        }
        catch(e)
        {
            throw error(400, `${e}`)
        }
    }
} 