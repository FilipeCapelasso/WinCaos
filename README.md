# System Interface Stress Simulator (Projeto001)

Este projeto é uma aplicação experimental desenvolvida em C# para explorar a manipulação
de componentes de baixo nível do Windows e gerenciamento de threads.

🛠 Destaques Técnicos
Win32 API (P/Invoke)
Manipulação direta do kernel para controle de cursor.
Low-Level Hooks: Implementação de filtros globais de teclado
Multithreading: Gerenciamento de janelas em threads independentes (STA Mode).
Persistência & Automação: Gerenciamento de chaves de Registro e execução de comandos de sistema via Process.
Protocolo de Emergência: Implementação de um Kill-Switch para restauração instantânea do ambiente.
Nota: Desenvolvido para fins de pesquisa em cibersegurança e resiliência de sistemas operacionais
