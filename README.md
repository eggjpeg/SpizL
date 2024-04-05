# SpizL

## Overview
This project aims to develop a custom programming language using C# that includes both an interpreter and elements of a compiler. The language will be designed to support a set of features suitable for various programming tasks. The interpreter will be capable of executing code written in the custom language, while the compiler will translate the code into executable form.

## Features
- **Custom Language Syntax**: Define the syntax and semantics of the custom programming language.
- **Interpreter**: Develop a fully functional interpreter capable of executing code written in the custom language.
- **Compiler Elements**: Implement partial compiler functionalities to translate code into executable form.
- **Variable Declaration and Assignment**: Support for declaring and assigning variables.
- **Control Structures**: Include control structures such as loops and conditional statements.
- **Functions**: Enable the definition and invocation of functions.
- **Data Types**: Support for various data types including integers, floats, strings, and arrays.
- **Input/Output Operations**: Implement mechanisms for input and output operations.
- **Error Handling**: Incorporate error handling mechanisms to provide meaningful feedback to users.

## Project Structure
1. **Lexer**: Tokenizes the input code into a stream of tokens.
2. **Parser**: Parses the token stream to build an abstract syntax tree (AST).
3. **Interpreter**: Evaluates the AST to execute the program.
4. **Compiler**: Translates the AST into executable code or intermediate representation.
5. **Tests**: Includes unit tests to ensure the correctness of the interpreter and compiler functionalities.
6. **Documentation**: Provides comprehensive documentation including language specifications, usage guidelines, and examples.

## Dependencies
- C# (.NET Core or .NET Framework)
- (Optional) NUnit or MSTest for testing

## Getting Started
1. Clone the repository
2. Navigate to the project directory: `cd CustomLanguageInterpreter`
3. Start experimenting with the custom language interpreter and compiler components.

## Usage
1. Write code in the custom language using a text editor.
2. Use the interpreter to execute the code directly or utilize the compiler to generate executable files.
3. Refer to the documentation for language specifications, syntax, and usage examples.

## Contributing
Contributions are welcome! If you want to contribute to this project, please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and ensure tests pass.
4. Commit your changes with descriptive commit messages.
5. Push your changes to your fork.
6. Submit a pull request to the main repository.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
