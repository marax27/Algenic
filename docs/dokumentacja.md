# Serwis z konkursami algorytmicznymi Algenic

## Wstępne wymagania

### Użytkownik
- Wysyła rozwiązania zadań konkursowych i jest za nie odpowiednio punktowany
- Po zakończeniu konkursu, każdy użytkownik może zobaczyć ranking
- Sformatowanie standardowego wyjścia programu leży po stronie użytkownika wysyłającego rozwiązanie, np. jeśli akceptowane rozwiązanie jest postaci "x y", gdzie x i y to liczby, to użytkownik powinien zadbać, aby wyjście jego programu przyjmowało taką formę
- Rozwiązanie zadania powinno być wysyłane przez użytkownika jako plik z kodem

### Organizator konkursów
- Można założyć, że jest tylko 1 organizator konkursów
- Tylko organizator konkursów może tworzyć nowe konkursy i dodawać do nich zadania
- Dodaje odpowiednio sformatowane przypadki testowe do zadań, względem których jest porównywane standardowe wyjście programu użytkownika
- Ustala ilość punktów otrzymanych przez użytkownika za rozwiązanie (w pełni lub częściowe) zadania

### Serwer/administrator
- Kompiluje kod użytkownika, ocenia pod względem poprawności i zwraca odpowiedni feedback użytkownikowi
- Przechowuje standardowe wyjście błędu i loguje je, w razie potrzeby wyświetla użytkownikowi
- Przechowuje historie wrzuconych rozwiązań
- Dba o podstawowe kwestie bezpieczeństwa - nie przechowuje haseł jako plaintext, uniemożliwia SQL injection

### Inne
- System antyplagiatowy nie jest konieczny
- Kwestia prezentacji projektu - czy na serwerze zewnętrznym (dostęp przez internet), czy na własnej maszynie, wybór jest dowolny

## Definicje przypadków użycia

**Przypadek użycia:** Rejestracja  
**Aktorzy:** Użytkownik  
**Zakres:** Serwis z konkursami algorytmicznymi Algenic  
**Cel (story):** Użytkownik chce się zarejestrować  
**Warunki początkowe:** Użytkownik nie jest zarejestrowany  
**Warunek końcowy dla powodzenia:** Użytkownik zostaje zarejestrowany  
**Warunek końcowy dla niepowodzenia:** Użytkownik nie zostaje zarejestrowany, zostaje wyświetlony komunikat o niepowodzeniu
**Zdarzenie wyzwalające (trigger):** Użytkownik wybiera funkcję rejestracji  
**Scenariusz główny:**
1. Serwiś wyświetla formularz rejestracji
1. Użytkownik wprowadza email oraz hasło (email służy jako login)
1. Następuje weryfikacja wprowadzonych danych po stronie serwisu
1. Użytkownik zostaje zarejestrowany. Koniec przypadku użycia.

**Rozszerzenia scenariusza głównego:**  
3a. Weryfikacja zakończona niepowodzeniem (email już istnieje w bazie, lub hasło nie spełnia warunków)  
3a1. Zostaje wyświetlony komunikat o niepowodzeniu. Koniec przypadku użycia.

**Przypadek użycia:** Logowanie  
**Aktorzy:** Użytkownik  
**Zakres:** Serwis z konkursami algorytmicznymi Algenic  
**Cel (story):** Użytkownik chce się zalogować  
**Warunki początkowe:** Użytkownik jest w stanie niezalogowanym  
**Warunek końcowy dla powodzenia:** Użytkownik zmienia stan na zalogowany  
**Warunek końcowy dla niepowodzenia:** Użytkownik nie zmienia stanu na zalogowany, zostaje wyświetlony komunikat o błędzie  
**Zdarzenie wyzwalające (trigger):** Użytkownik wybiera funkcję zalogowania  
**Scenariusz główny:**  
1. Serwis wyświetla formularz do logowania
1. Użytkownik wprowadza login oraz hasło
1. Następuje autentykacja danych po stronie serwisu
1. Stan użytkownika zostaje zmieniony na zalogowany. Koniec przypadku użycia.
  
**Rozszerzenia scenariusza głównego:**  
3a. Autentykacja nie powiodła się  
3a1. Serwis wyświetla komunikat o błędzie. Koniec przypadku użycia.

**Przypadek użycia:** Dodaj rozwiązanie zadania  
**Aktorzy:** Użytkownik  
**Zakres:** Serwis z konkursami algorytmicznymi Algenic  
**Cel (story):** Dodanie rozwiązania do zadania  
**Warunki początkowe:** Użytkownik jest uczestnikiem aktywnego konkursu, konkurs posiada przynajmniej jedno (nierozwiązane przez użytkownika) zadanie  
**Warunek końcowy dla powodzenia:** Zadanie zostaje rozwiązane, użytkownik zostaje powiadomiony o poprawnym wykonaniu zadania  
**Warunek końcowy dla niepowodzenia:** Zadanie nie zostaje rozwiązane, użytkownik zostaje powiadomiony o niepowodzeniu oraz rodzaju błędu  
**Zdarzenie wyzwalające (trigger):** Użytkownik wybiera zadanie z konkursu, którego jest uczestnikiem i wybiera funkcję dodania rozwiązania zadania  
**Scenariusz główny:**
1. Użytkownik wybiera konkurs
1. Użytkownik wybiera zadanie
1. Serwis wyświetla treść zadania i udostępnia możliwość wrzucenia rozwiązania w postaci pliku z rozwiązaniem
1. Użytkownik wrzuca plik z rozwiązaniem zadania
1. Serwis wielokrotnie kompiluje plik z rozwiązaniem z różnymi danymi wejściowymi
1. Serwis porównuje dane wyjściowe kompilacji z poprawnymi rozwiązaniami zadania
1. Serwis zapisuje plik oraz wynik kompilacji (sukces/treść błędu) w historii
1. Serwis zwraca wiadomość o poprawności wykonania zadania (ilości zaliczonych testów). Koniec przypadku użycia.
  
**Rozszerzenia scenariusza głównego:**   
5a. Kompilacja nie powiodła się  
5a1. Serwis zwraca wiadomość o niepowodzeniu kompilacji wraz z treścią błędu. Koniec przypadku użycia.

**Przypadek użycia:** Utwórz konkurs  
**Aktorzy:** Organizator konkursów  
**Zakres:** Serwis z konkursami algorytmicznymi Algenic  
**Cel (story):** Organizator konkursów chce utworzyć nowy konkurs  
**Warunki początkowe:** Organizator konkursów jest zalogowany  
**Warunek końcowy dla powodzenia:** Konkurs został utworzony  
**Warunek końcowy dla niepowodzenia:** Konkurs nie został utworzony  
**Zdarzenie wyzwalające (trigger):** Organizator konkursów wybiera opcję utworzenia nowego konkursu  
**Scenariusz główny:**  
1. Organizator konkursów wybiera funkcję utworzenia nowego konkursu
1. Serwis wyświetla formularz utworzenia nowego konkursu
1. Organizator konkursu wprowadza nazwę konkursu
1. Zostaje utworzony nowy konkurs w stanie "nierozpoczęty". Koniec przypadku użycia.

**Rozszerzenia scenariusza głównego:**

**Przypadek użycia:** Dodaj zadanie do istniejącego konkursu  
**Aktorzy:** Organizator konkursów  
**Zakres:** Serwis z konkursami algorytmicznymi Algenic  
**Cel (story):** Organizator konkursów chce dodać zadanie do konkursu  
**Warunki początkowe:** Organizator konkursów ma możliwość dokonywania zmian w konkursie w stanie "nierozpoczęty"  
**Warunek końcowy dla powodzenia:** Zadanie zostało dodane do konkursu  
**Warunek końcowy dla niepowodzenia:** Zadanie nie zostało dodane do konkursu, zostaje wyświetlony komunikat o niepowodzeniu  
**Zdarzenie wyzwalające (trigger):** Organizator konkursów wybiera konkurs w stanie "nierozpoczęty" i wybiera opcję dodania zadania do konkursu   
**Scenariusz główny:**  
1. Organizator konkursów wybiera konkurs w stanie "nierozpoczęty"
1. Organizator konkursów wybiera funkcję dodania zadania do konkursu
1. Serwis wyświetla formularz do dodania nowego zadania
1. Organizator konkursów wypełnia formularz
1. Nowe zadanie zostaje dodane do konkursu. Koniec przypadku użycia.

**Rozszerzenia scenariusza głównego:**  
4a. Formularz został wypełniony nieprawidłowo  
4a1. Zostaje wyświetlony komunikat o niepowodzeniu. Koniec przypadku użycia.

## Architektura systemu

### Podział na moduły



### Główna część aplikacji (???)

W celu efektywnego odseparowania logiki aplikacji programu od interfejsu graficznego, zdecydowaliśmy się na użycie wzorca MVVM. Pozwala on na rozdzielenie aplikacji części:

1. Model -  stanowi reprezentację danych przechowywanych przez serwis
1. View  - nasz interfejs graficzny, wyświetla dane reprezentowane przez Model
1. Viewmodel - reprezentuje obecny stan Modelu, udostepnia dane dla View

Szczególną cechą tego wzorca w porównaniu do jemu podobnych (MVC, MVP), jest to, że Viewmodel docelowo nie wie o istnieniu View. Do wyświetlenia obecnego stanu programu w View wykorzystywany jest data binding - elementy GUI są zsynchronizowane z danymi udostępnianymi przez Viewmodel. Zmiany następujące w Modelu powodują zmiany w interfejsie graficznym użytkownika.

Docelowo, Model zostanie wygenerowany na podstawie wcześniej utworzonej bazy danych za pomocą funkcji Scaffolding należącej do Entity Framework Core.  
Do stworzenia View zostanie zastosowany ASP.NET Razor Pages - przystępny framework do tworzenia stron internetowych.  
Każda strona Razor Pages będzie miała własną klasę, która odpowiada za udostępnianie jej danych modelu (tzw. code behind). Jest to wcześniej wspomniany ViewModel.

Rozdzielenie aplikacji na powyższe trzy części zdecydowanie pomoże w unikaniu silnych powiązań w kodzie podczas implementacji.
