🎟️ IssueTicketService – Projekt R&D

Projekt badawczo-rozwojowy typu Helpdesk / Ticket Service mający na celu sprawdzenie efektywności technologii i wpływu na wydajną pracę pojedynczego Full-Stack Developera odpowiedzialnego za wszystkie składowe aplikacji.
🛠️ Użyte kluczowe technologie

    Backend: .NET Core 8 (API, Clean Architecture)

    ORM: Entity Framework Core 8

    Frontend: SvelteKit 5

    Baza Danych: PostgreSQL

    Infrastruktura: Docker + Docker Compose, Nginx (Reverse Proxy)

📍 Aktualny status projektu

Projekt znajduje się nadal w fazie rozwoju. Obecne prace skupiają się na:

    Tworzeniu interfejsu użytkownika i spinaniu go z API.

    Dostosowaniu kontenerów Docker do pełnego funkcjonowania w sposób niezależny na dowolnej maszynie.

    Wystawieniu gotowego projektu na serwerze VPS z użyciem Nginx jako reverse proxy.

Wnioski z projektu

.NET Core 8: To wydajna i dająca świetny Developer Experience platforma do tworzenia nowoczesnych API. Jednak użycie Clean Architecture dla pojedynczego dewelopera generuje ogromny narzut kodu (boilerplate), który wraz ze wzrostem aplikacji niepotrzebnie utrudnia i spowalnia codzienne prace.

EF Core 8: To efektywne narzędzie do pracy z bazami danych, jednak rygory i ograniczenia nałożone przez wybraną architekturę nie pozwoliły w pełni wykorzystać jego potencjału, tworząc miejscami sztuczne bariery techniczne.

SvelteKit 5: To przejrzysty i lekki framework, który sprawdził się znakomicie – jest idealny zarówno do budowy szybkich prototypów, jak i zaawansowanych aplikacji webowych.

PostgreSQL: Wydajna, stabilna a przede wszystkim darmowa do użytku w dowolnym projekcie bez narzucanych ograniczeń jak w SQL Server Express. Bardzo dobrze współgra z ORMami jak i .NET.

Docker i Docker Compose: Przy tworzeniu nowoczesnych, wielowarstwowych technologicznie aplikacji, są to narzędzia niemal niezbędne i ułatwiajace pracę. Trzeba nabyć wprawy w ich konfigurowaniu, jednak to co oferują w zamian to rozwiązanie wielu bolączek ówczesnych lat.

Użyte technologie byly bardzo przystępne do nauki i zachęciły do dalszego rozwijania w nich swoich umiejętności. Co da się jednak zauważyć przewija się w nich ból związany z użyciem Clean Architecture. 
Podczas tworzenia samego API używając Clean Architecture każde dodanie nowej funkcjonalności wiązało się z dodaniem szporej ilości kodu będącego sztucznym nakładem wymaganym przez architekturę. Ten kod pozostaje z nami do końca projektu, więc dla solo developera oznacza to dzwiganie go do samego końca lub przy zmianach skrupulatne usuwanie danej funkcjonalności z każdej warstwy.
Trzymając się sztywnych ram architektury jesteśmy zmuszani do wytworzenia większej ilości kodu dla zachowania spójności niż samego kodu, który dostarczy realną wartość rynkową.
Z doświadczenia również miałem okazję przekonać się, że ta architektura nie sprawdza się nie tylko dla solo developera ale również dla mniejszych zespołów. Tworząc wielowarstwowe monolityczne mikroserwisy można utonąć w ilości mapowanych obiektów przekazywanych między warstwami w kilku projektach nim wyświetli się porządaną jedną zmianę na ekranie.
Tworzą się również problemy z pełnym wykorzystaniem potencjalu ORM. Najczęściej w tej architekturze EF Core zamykany jest w warstwie repozytorium, po czym okazuje się, że przez warstwowy układ architektury i duży podziął odpowiedzialności między warstwami nie mamy możliwości pobrania i wyświetlenia danych w dowolny sposób bez łamania konwencji architektury. 
Wymusza ona również często nieefektywne pobieranie całego obiektu wraz ze wszystkimi jego składowymi, np. Pobranie użytkownika wraz z jego 100 ticketami i 800 komentarzami aby zachować ustaloną u podstaw spójność architektury.

Czy jest to więc przestarzała architektura? W swojej podstawowej i niedostosowanej do współczesnych czasów owszem. Nie polecałbym również wykorzystania jej w uwspółcześnionej wersji, jeśli zespół programistów nie przekracza około 30 osób. Nie jest to architektura stworzona dla pojedynczych developerów ani małych zespołów, jednak ma ona swoje zastosowanie w specyficznych warunkach.
