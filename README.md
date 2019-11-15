![Travis (.org)](https://img.shields.io/travis/marax27/Algenic)
![GitHub pull requests](https://img.shields.io/github/issues-pr/marax27/Algenic)
[![License](https://img.shields.io/badge/License-BSD%203--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)

# Algenic
Web application for online programming competitions

*Projekt zaliczeniowy z przedmiotu Inżynieria Oprogramowania.*

## Setup


## Testing

Recommended workflow:
1. Open terminal and navigate to `Algenic\Algenic` (inside the Algenic **project**).
2. `dotnet watch run`
3. The application should now be available at *http://localhost:5000*.
4. In Visual Studio, run tests.
5. Whenever you edit source files (only *.cs*, it seems), the application will restart. Then you can run the tests again.
6. Functional tests will use a browser specified in *testSettings.json*. Change `TestBrowser` setting to choose your favourite browser ("Firefox", "Chrome", "Opera", ...), but try not to commit updated setting to the repository.
