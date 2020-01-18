# Algenic

## Struktura projektu
Algenic składa się z następujących projektów:
- *Algenic:* pliki aplikacji sieciowej (kod HTML, CSS itd.), logika aplikacji.
- *Algenic.Commons:* podstawowe interfejsy, narzędzia niebędące związane ze specyfiką aplikacji.
- *Algenic.Compilation:* stanowi warstwę abstrakcji w procesie kompilacji kodu. Zawiera przede wszystkim klasy pozwalające na komunikację z API serwisu JDoodle.
- *Algenic.UnitTests:* testy jednostkowe.
- *Algenic.FunctionalTests:* testy Selenium.

## Struktura strony internetowej
- Strona główna: zalogowany użytkownik może tutaj obejrzeć wyniki konkursów, w których brał udział.
- `/ScorePolicies`: służy tworzeniu nowych polityk oceniania. Dostęp tylko dla egzaminatorów.
- `/Contests`: umożliwia przeglądanie, dołączanie do istniejących konkursów. Egzaminatorzy mogą dodatkowo tworzyć nowe konkursy bądź edytować istniejące.
- `/Admin`: panel administratora


## Testy automatyczne
Testy automatyczne w projekcie dzielą się na 2 grupy: jednostkowe i funkcjonalne.

### Testy jednostkowe
Aby ułatwić testowanie, operacje na bazie danych zostały wydzielone i zawarte w tzw. handlerach. QueryHandler ma za zadanie pobrać dane z bazy i zwrócić wynik, natomaist CommandHandler dodaje nowe dane bądź modyfikuje istniejące wpisy. Dla każdego testu tworzona jest w pamięci operacyjnej (in-memory) tymczasowa baza danych, zatem testy nie wpływają ani na siebie, ani na oryginalną bazę.

### Testy funkcjonalne
Testy funkcjonalne wykorzystują 3 konta użytkowników, tworzonych domyślnie, gdy aplikacja jest uruchamiana w trybie developerskim.

Obecne testy funkcjonalne badają następujące funkcjonalności programu:
1. Dostęp do panelu administratora
    - jako zwykły użytkownik (brak dostępu)
    - jako egzaminator (brak dostępu)
    - jako administrator (dostęp przyznany)

2. Dodawanie konkursów
    - egzaminator tworzy nowy konkurs
    - dane nowego konkursu powinny pojawić się w tabeli konkursów

3. Uprawnienia do dodawania konkursów
    - jako egzaminator (pojawia się odpowiedni formularz)
    - jako zwykły użytkownik (brak formularza na stronie)

4. Zmiana właściciela konkursu przy odebraniu uprawnień egzaminatora obecnemu właścicielowi konkursu
    - *patrz: ContestOwnershipTransferOnPrivilegeChange.cs, wszystkie kroki są tam opisane. Nazwy metod z góry do dołu*
