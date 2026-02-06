WinChaos - Educational System Simulation (Projeto001)

Este projeto é uma **simulação de comportamento de sistema** desenvolvida em C# para fins estritamente educacionais e de estudo sobre a API do Windows (Win32). O objetivo é demonstrar como interagir com periféricos, registros e janelas do sistema.

## 🔴 AVISO DE SEGURANÇA
**Este software NÃO é um vírus real**, mas utiliza técnicas comuns em malwares de simulação (pranks) para fins de aprendizado.
* O antivírus (Windows Defender) provavelmente irá detectá-lo como ameaça.
* Recomenda-se a execução apenas em ambientes controlados (Máquinas Virtuais).
* O autor não se responsabiliza por uso indevido deste código.

## 🛠️ Funcionalidades Técnicas
O projeto explora diversas bibliotecas profundas do ecossistema Windows:

* **Manipulação de Periféricos:** Uso de `user32.dll` para controle forçado do cursor do mouse e rastro (mouse trails).
* **Hooks de Teclado:** Bloqueio de teclas de sistema como `Alt+Tab`, `WinKey` e `Esc` através de `SetWindowsHookEx`.
* **Persistência:** Simulação de inserção no Registro do Windows (`CurrentVersion\Run`) para inicialização automática.
* **Interface Assíncrona:** Gerenciamento de múltiplas janelas pop-up usando `Task.Run` e `Threading`.
* **Comandos de Shell:** Integração com processos de desligamento do sistema para demonstração de comandos críticos.

## 🚀 Como Executar
1. Baixe o arquivo `.zip`.
2. Extraia o conteúdo (Mantenha as DLLs na mesma pasta do executável).
3. Execute o `WinChaos.exe`.
4. **Para encerrar a simulação:** Pressione `Ctrl + Shift + F12` (ou a combinação de teclas configurada no código).

## 🔴 Como Encerrar o Programa (Emergency Exit)
Se você rodou o programa e o mouse começou a pular ou as janelas travaram tudo, não entre em pânico. O código possui um "Interruptor de Emergência":

Mantenha pressionadas as teclas: CTRL + SHIFT
Enquanto segura elas, pressione a tecla: F12

O programa irá:
Restaurar a velocidade normal do mouse.
Remover os rastros do ponteiro.
Liberar os ganchos (hooks) do teclado.
Encerrar todos os processos do WinChaos imediatamente.

## 💻 Tecnologias Utilizadas
* C# 10 / .NET 6.0
* Windows Forms (WinForms)
* P/Invoke (Win32 API Interop)
