Descrição
Este projeto é uma aplicação experimental desenvolvida em C# que explora a manipulação de componentes de baixo nível do Windows (Win32 API). O objetivo principal é demonstrar o controle programático de periféricos (mouse), interceptação de eventos de teclado (Hooks) e gerenciamento dinâmico de múltiplas threads de interface gráfica (Windows Forms).

Originalmente concebido como um exercício de Cibersegurança e UX Design, o programa simula um cenário de "falha crítica de sistema" para testar a resiliência da interface e a resposta do sistema operacional a processos persistentes.

🛠 Tecnologias e Conceitos Utilizados
P/Invoke & Win32 API: Interação direta com o kernel do Windows para manipulação de cursor, trilhas de movimento e parâmetros globais do sistema (user32.dll).
Low-Level Keyboard Hooks: Implementação de um Hook global para filtragem de teclas de sistema (Alt+Tab, WinKey, etc.), demonstrando conhecimento em segurança e controle de input.
Multithreading & Task Parallel Library (TPL): Gerenciamento de múltiplas threads em modo STA (Single Thread Apartment) para criação dinâmica e recursiva de janelas.
Manipulação de Registro (Registry): Implementação de persistência local para inicialização automática com o sistema.
UI/UX Customizada: Criação de interfaces "borderless" com feedback visual e sonoro em tempo real.

🚀 Funcionalidades Técnicas
Simulação de Movimento Caótico: Algoritmo de jitter randômico aplicado ao cursor do sistema.
Gerenciamento de Persistência: Escrita em chaves de Run do Registro do Windows para estudo de ciclo de vida de aplicações.
Protocolo de Encerramento Forçado: Demonstração de execução de comandos de sistema via ProcessStartInfo com privilégios de execução.
Kill-switch de Emergência: Atalho de segurança implementado via monitoramento de estado de teclas (GetKeyState) para restauração imediata dos parâmetros originais do sistema.

⚠️ Disclaimer (Aviso Legal)
Este projeto possui fins estritamente educacionais e de demonstração técnica. Ele foi criado para ilustrar como aplicações interagem com o sistema operacional em nível profundo. Não deve ser utilizado de forma maliciosa ou sem consentimento.
