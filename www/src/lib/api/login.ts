import { env } from "$env/dynamic/private";
import type { LoginDto } from "$lib/types/Login/LoginDto";
import type { Tokens } from "$lib/types/Login/Tokens";

export type LoginResult =
  | { ok: true; tokens: Tokens }
  | { ok: false; status: number; message: string };

const failure = (status: number, message: string): LoginResult => ({ ok: false, status, message });

export const logIn = async (data: FormData): Promise<LoginResult> => {
    const credentials: LoginDto ={
        email: data.get('email')?.toString().trim() ?? null,
        password: data.get('password')?.toString() ?? null
    }

  if (!credentials?.email || !credentials?.password) return failure(400, 'Podaj e-mail i hasło');

  let res: Response;
  try {
    res = await fetch(`${env.INTERNAL_API_URL}/Authentication/Login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(credentials)
    });
  } catch (e) {
    console.error('Login: API niedostępne', e);
    return failure(502, 'Błąd połączenia z serwerem');
  }

  if (!res.ok) {
    const err = await res.json().catch(() => ({}));
    return failure(res.status, err.message || 'Błędne dane logowania');
  }

  const tokens: Partial<Tokens> | undefined = (await res.json().catch(() => null));
  if (!tokens?.authToken || !tokens?.refreshToken) {
    console.error('Login: nieoczekiwany format, klucze:', tokens ? Object.keys(tokens) : tokens);
    return failure(502, 'Nieprawidłowa odpowiedź serwera');
  }

  return { ok: true, tokens: { authToken: tokens.authToken, refreshToken: tokens.refreshToken } };
};