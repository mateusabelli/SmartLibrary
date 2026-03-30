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
- [Development](#development)
  - [Initial Setup](#initial-setup)
  - [Applying Manifests](#applying-manifests)
  - [Running Locally](#running-locally)
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

## Development

Prerequisites:
- docker
- kubectl
- minikube

### Initial setup

After you've started the Kubernetes cluster in Minikube, proceed with the following steps:

Create the database password secret: `DBPassword12!`
```bash
kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD='DBPassword12!'
```

Installing Envoy [Docs](https://gateway.envoyproxy.io/docs/tasks/quickstart/#installation)
```bash
# Install the Gateway API CRDs and Envoy Gateway:
helm install eg oci://docker.io/envoyproxy/gateway-helm --version v1.7.1 -n envoy-gateway-system --create-namespace

# Wait for Envoy Gateway to become available:
kubectl wait --timeout=5m -n envoy-gateway-system deployment/envoy-gateway --for=condition=Available

# Install the GatewayClass, Gateway, HTTPRoute and example app:
kubectl apply -f https://github.com/envoyproxy/gateway/releases/download/v1.7.1/quickstart.yaml -n default
```

Installing RabbitMQ Operator [Docs](https://www.rabbitmq.com/kubernetes/operator/quickstart-operator)

```bash
# Install the RabbitMQ Cluster Operator
kubectl apply -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"

# Apply the rabbit-mq-depl inside the "K8S" folder
kubectl apply -f K8S/rabbitmq-depl.yaml

# Get the required credentials for Production build
username="$(kubectl get secret rabbitmq-depl-default-user -o jsonpath='{.data.username}' | base64 --decode)"
echo "username: $username"
password="$(kubectl get secret rabbitmq-depl-default-user -o jsonpath='{.data.password}' | base64 --decode)"
echo "password: $password"

# Simulated output (it will be random)
# username: default_user_lMCS4yswJGo-GKiOCh9
# password: 9gqfjaQ7YzBCSShNq8jn32d8cOHxOEUn
```

With that generated username and password, update the `appsettings.Production.json` in InventoryService and LendingService

### Applying manifests

Run the following command to apply all the manifests in the "K8S" folder
```bash
kubectl apply -f K8S
```

This will do the following operations (not in the same order):

- Create the Gateway with Envoy mapped to the Pods Cluster Ips
- Create the Pod with Cluster IP for the InventoryService and LendingService
- Create MSSQL Database with its own LoadBalancer for external access
- Create the RabbitMQ Deployment
 
> [!NOTE]
> run `kubectl get all` to check if any service is `0/1`, if so, it might be needed to restart them.

### Running locally

Using an API client such as Insomnia or Postman. Try to run the following operations to check if everything is running okay.

List books from the inventory service:
```bash
GET http://localhost:8123/api/inventory
```

Choose a book from previous result, for example: book with `id: 1` and fetch from the lending service:
```bash
GET http://localhost:8123/api/books/4
```

Create a lend request with `BookId` (from the inventory) and `Borrower` name:

```bash
POST http://localhost:8123/api/lending/4
# body JSON:
# {  
#   "BookId": 4,
#   "Borrower": "Mateus"
# }
```

If everything is running properly the services should be communicating via the message bus and grpc. If you keep
creating lending requests and the book runs out of stock, it should return a `200` status code with the message
"out of stock".

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