# README #

### What is this repository for? ###

* This repository contains the solution for a Package Challenge given by Mobiquity. 

### What are we using? ###

* We are going with dependency injection, specifically Constructor Injection to manage our dependencies. 
* We're probably going a little overboard on the separation of Interfaces for each service where they only have a single reference and a single method. But again for the sake of scalability (and the correct implementation of constructor injection) I decided to showcase it.

### Pitfalls and challenges? ###

* I was going to use a Dependency Injection container framework to manage our dependencies. I prefer this in general as opposed to Constructor Injection as long chains of concrete instantiations in constructors looks messy to me. However, due to the fact that it is not good practice to configure registries for a DI container outside of the Composition Root of an application, and the other fact that Libraries should not have composition roots, it seemed like a bad idea.

* I was reminded of the benefits of TDD as I wrote my parsing method to get the file contents into class objects before writing any tests. What ended up happening is I passed the file path into the parse method, forcing it to first fetch the file into a stream (which violated the single responsibility principle as that should not be the parser's job). This meant when I started writing a unit test for it I could not test it's main job without creating a dummy file each test. After I slapped myself on the wrists I changed the parser to take in a string and simply parse it, making our tests nice and unit testy.

### How did we get here? ###

* The first port of call was to create the basic structure of the solution in draw.io


