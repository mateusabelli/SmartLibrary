# Smart Library

A library distributed system built with microservices for book management and lending.

## Table of Contents

- [Overview](#overview)
- [Motivation](#motivation)
  - [Challenges](#challenges)
- [Usage](#usage) (TODO)
- [System Architecture](#system-architecture) (TODO)
    - [Diagram](#diagram) (TODO)
    - [Key Services](#key-services) (TODO)
- [Tech Stack](#tech-stack) (TODO)
- [Development](#development) (TODO)
- [How to Contribute](#how-to-contribute)
- [License](#license)

## Overview

The Smart Library project has two main services, one for book management, allowing you to include new books and search.
The other allows users to create a lend request for a book. The microservices for book inventory and lending work
individually, each with its own database, and they communicate internally by using events and gRPC.

## Motivation

This project has been developed as a learning practice after studying the microservices course by Les Jackson available
on YouTube. It has been a great addition to the studies since I had to figure out a few things and edge cases.

### Challenges

- **Migrating from Automapper to Mapperly:** The newer versions of Autommaper includes an awful warning in the console
regarding their licensing, I disliked that behavior so I had to figure out how to implement Mapperly instead.

- **RabbitMQ is now Async by default**: I had to learn how to work with `BackgroundServices` in .NET to properly add
the message bus on the project. I also wanted it to **not** hang the thread when the service was starting up, so it was a 
great learning experience.
 
- **Nginx Ingress is deprecating soon**: The new recommended thing is the Gateway API, I struggled a bit with the way
they explained in the docs, saying it has now 3 personas, and stuff like that. In reality is just a bit more modular, 
I'm glad I managed to get that up and running.

## How to Contribute

All contributions are welcome and much appreciated!

- Take a look at the existing [Issues](https://github.com/mateusabelli/smartlibrary/issues) or
[create a new issue](https://github.com/mateusabelli/smartlibrary/issues/new/)!
- [Fork the Repo](https://github.com/mateusabelli/smartlibrary/fork). Then, create a branch for any issue that you are
working on. Finally, commit your work.
- Create a **[Pull Request](https://github.com/mateusabelli/omedev/compare)** (_PR_), will be reviewed and given
feedback as soon as possible.

> [!NOTE]
> There are other ways of contributing. [Read more](https://github.com/mateusabelli/.github/blob/main/CONTRIBUTING.md)

## License

This project is under the MIT license, read the [LICENSE](LICENSE.md) file for more details.