# SpizL

## Explanation
This project aims to develop a custom programming language "SpizL" using C# that includes both an interpreter and the beginnings of a compiler. The purpose of this project was for me to learn programming since I believe that writing a language allows you to gain a very good understanding of any programming language. This was developped in my senior year of high school. I wasn't aware of github back then so everything has just been freshly pushed.

## Motivation
I like a lot of the features in C# but there were many mysteries to me as to why things are the way they are in programming languages. I wanted to play around with my own language to have some unique functionality that I haven't seen in any others. The language is designed to support basic programs with most of the basic assignment operators and commands. 

## Features
- **Custom Language Syntax**: I have my very own syntax.
- **Interpreter**: Develop a fully functional interpreter capable of executing code written in the custom language.
- **Compiler Elements**: Implement partial compiler functionalities to translate code into assembly and into executable form.
- **Variable Declaration and Assignment**: Support for declaring and assigning variables.
- **Control Structures**: Include control structures such as loops and conditional statements.
- **Functions**: Enable the definition and invocation of functions.
- **Data Types**: Support for various data types including integers, strings, and lists.
- **Error Handling**: Incorporate error handling mechanisms to provide meaningful feedback to users.


## Special Features
- **dospiz**: This is the equivalent of a loop. It can be a for, while, or infinite loop depending on what follows. I never liked having while (1 == 1) loops so I decided to allow "dospiz" as a command itself which is an infinite loop.
- **spizout**: This is the equivalent of a break. My innovation with this was to allow a number of breaks to come after. This allows you to break out of any number of nested loops in one command. 
- **lint**: This is a list of ints. I liked being able to add simply by calling "spad" with the list followed by its contents.
- **.**: Why hasn't any language thought of this? In SpizL, functions, loops and if statements end with a . 

## Project Structure
1. **Lexer**: Tokenizes the input code into a stream of tokens.
2. **Parser**: Parses the token stream to build an abstract syntax tree (AST).
3. **Traversal**: Evaluates the AST to execute the program.
4. **Compiler**: Translates the AST into executable code or intermediate representation.
5. **Tests**: Includes unit tests to ensure the correctness of the interpreter and compiler functionalities.

## Dependencies
- C# (.NET Core or .NET Framework)
- (Optional) NUnit or MSTest for testing

## Getting Started
1. Clone the repository
2. Navigate to the project directory: `cd SpizL/spizl`
3. There are code files there which will help you understand the language better, it is fairly intuitive.

## Usage
1. Play with the files in the Spizl/spizl folder
2. Create your own files and run them in SpizL!
3. The compiler is not working yet but it does generate assembly code.


