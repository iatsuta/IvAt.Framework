# Copilot Instructions

## Project Guidelines
- In tests, prefer direct Act/Assert patterns: use Record.ExceptionAsync(...)/Assert.Null(ex) for no-exception assertions instead of storing delegates like var action = ... and then awaiting/invoking them in Assert.