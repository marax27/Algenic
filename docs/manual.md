
*screenshoty mile widziane*

# Role

## Administrator
Podstawową czynnością administracyjną jest przydzielanie uprawnień Egzaminatora. Jest to możliwe z poziomu Panelu Administratora dostępnego przez pasek menu u góry strony (dostęp tylko dla zalogowanego Administratora). Aby przydzielić/odebrać użytkownikowi prawa Egzaminatora, należy nacisnąć odpowiedni przycisk obok nazwy użytkownika.

Uwaga: odbierając uprawnienia Egzaminatora wszystkie jego konkursy zmienią właściciela: nowym właścicielem zostanie Administrator. Odpowiednie ostrzeżenie wyświetli się po najechaniu na przycisk "Revoke".

## Egzaminator
Egzaminator może:
- tworzyć konkursy
- usuwać swoje konkursy
- zmieniać stan swojego konkursu (Not started, In progress, Completed)
- edytować swoje (nierozpoczęte) konkursy, w tym dodawać, edytować i usuwać zadania

## Każdy użytkownik
Może:
- wysyłać rozwiązania do zadań z cudzych konkursów
- przeglądać wyniki swoich rozwiązań (po zakończeniu konkursu)


# Korzystanie z aplikacji

## Instalacja
Wymagania:
- .NET Core SDK 2.2 lub nowszy
- Visual Studio 2017 lub nowszy (opcjonalnie)

## Uruchomienie
Aby uruchomić aplikację, można skorzystać z konsolowego narzędzia dotnet. Należy w tym celu:
1. Przejść do katalogu, w którym znajduje się projekt.
2. Wejść do katalogu `Algenic/Algenic`.
3. Wykonać polecenie `dotnet run`.

## Testy jednostkowe
1. Upewnić się, że aplikacja nie jest uruchomiona np. poprzez dotnet.
2. Uruchomić Visual Studio.
3. Z poziomu okna Test Explorer wybrać *Algenic.UnitTests > Run*.

## Testy funkcjonalne
1. Upewnić się, że co najmniej jedna ze wspieranych przeglądarek (Firefox, Chrome, Opera) jest zainstalowana. Wybrać odpowiednią z nich w pliku konfiguracyjnym `Algenic/Algenic.FunctionalTests.testSettings.json`.
2. Dla wybranej przeglądarki pobrać WebDriver, który pozwoli frameworkowi testowemu komunikować się z przeglądarką. Pobrany WebDriver (np. operadriver.exe) umieścić w katalogu `Algenic/Algenic.FunctionalTests/bin/Debug/netcore2.2`.
3. Uruchomić aplikację z poziomu narzędzia dotnet (`dotnet run`).
4. W Visual Studio, w oknie Test Explorer, wybrać *Algenic.FunctionalTests > Run*.
