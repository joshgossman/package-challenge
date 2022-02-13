# README #

### What is this repository for? ###

* This repository contains the solution for a Package Challenge given by Mobiquity. 

### What are we using? ###

* We are going with dependency injection, specifically Constructor Injection to manage our dependencies. 

* We're probably going a little overboard on the separation of Interfaces for each service where they only have a single reference and a single method. But for the sake of scalability (and the correct implementation of constructor injection) I decided to showcase it.

* We are using the KnapSack algorithm to do the sorting of the package items.

### Pitfalls and challenges? ###

* I was going to use a Dependency Injection container framework to manage our dependencies. I prefer this in general as opposed to Constructor Injection as long chains of concrete instantiations in constructors looks messy to me. However, this has a lot of overhead for such a small solution. Furthermore it is not considered good practice to configure registries for a DI container outside of the Composition Root of an application, and since Libraries should not have composition roots, it seemed like a bad idea.

* I was reminded of the benefits of TDD as I wrote my parsing method to get the file contents into class objects before writing any tests. What ended up happening is I passed the file path into the parse method, forcing it to first fetch the file into a stream (which violated the single responsibility principle as that should not be the parser's job). This meant when I started writing a unit test for it I could not test it's main job without creating a dummy file each test. After I slapped myself on the wrists I changed the parser to take in a string and simply parse it, making our tests nice and unit testy.

* Figuring out the best way to solve the package sorting problem required considerable research. It could be bruteforced to go through every possibility, with a few optimisations. But I wanted to figure out something better. I discovered that this is in essence the Knapsack problem. With many solutions that simply needed personalisation and tweaking in order to get the output required.

### How did we get here? ###

* The first port of call was to create the basic structure of the solution in draw.io


