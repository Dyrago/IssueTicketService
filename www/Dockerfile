FROM node:latest as build

WORKDIR /svelte

COPY package*.json ./

#Uruchomienie tego przed skopiowaniem kodu źródłowaego zapewnia, że zależności zbudują się tylko wtedy,
#gdy rzeczywiście ulegną zmianie (add lub delete) inaczej są cachowane
RUN npm ci 

#Skopiowanie całego kodu źródłowego (po za tym zawarrtym w .dockerignore)
COPY . . 

# KOMPILACJA: SvelteKit uruchamia Vite i kompiluje aplikację.
# Wynikiem tego polecenia jest powstanie nowego, zoptymalizowanego folderu "build/"
RUN npm run build

#Nowy zestaw komend z lekkim runnerem do uruchomienia kodu źródłowego aplikacji
FROM node:current-alpine AS runner

WORKDIR /svelte

# Informacja dla node, że działamy w trybie produkcyjnym
ENV NODE_ENV=production

# Kopjujemy zbudowane pliki źródłowe z poprzedniego etapu
COPY --from=build /svelte/build ./build

# Kopjujemy zależności aby runner wiedział jakie pakiet są niezbędne
COPY --from=build /svelte/package*.json ./

# OPTYMALIZACJA CACHE: Ponownie instalujemy biblioteki, ALE z flagą --omit=dev.
# Oznacza to: "zainstaluj tylko to, co niezbędne do uruchomienia, pomiń Vite, kompilatory itp."
# Dzięki temu obraz produkcyjny drastycznie chudnie.
RUN npm ci --omit=dev

# Informujemy, że kontener będzie nasłuchiwał na porcie 3000
EXPOSE 3000
ENV PORT=3000

CMD ["node", "build"]