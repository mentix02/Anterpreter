# Anterpreter

An Assignment interpreter.

The project is loosely structured around the interpreter pattern where each "token" has
a corresponding class or method. In this case, there are two types of "token" - 

1. [Command](https://github.com/mentix02/Anterpreter/blob/master/Commands/ICommand.cs#L3)
2. [Exercise](https://github.com/mentix02/Anterpreter/blob/master/Exercises/IExercise.cs#L3)

Command names take precedence over exercises and can be executing with their first characters.

For example, to run the [Help](https://github.com/mentix02/Anterpreter/blob/master/Commands/Help.cs) command (which prints a list of commands), one can simply enter `h`.

To run an exercise, one may simple enter the exercise number (or the entire name).

```

   ___        __                       __
  / _ | ___  / /____ _______  _______ / /____ ____
 / __ |/ _ \/ __/ -_) __/ _ \/ __/ -_) __/ -_) __/
/_/ |_/_//_/\__/\__/_/ / .__/_/  \__/\__/\__/_/
                      /_/

...

> h
Commands -
- banner
- clear
- help
- listexercises
- quit
```
