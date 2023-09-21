# Notes from me
This test was done for Equal Experts. They weren't happy about the tests, but they do respect the the behavioural aspects of TDD. All the tests were done with great care. It's very loose from implementation details. 

I do think that in this kind of project, Unit Testing might not be suitable, though. I do think Integration Testing would be the only requirement, but I still did Unit and Integration to differentiate both. 

I've used the standard MSTest framework, which is not the standard for me, but it sufficed for the requirements. I've used Moq, and I haven't update myself on the current status of the [drama](https://dev.to/pbouillon/the-moq-gate-you-either-die-a-hero-8).

I didn't concern myself with using dotnet as it should (like injecting the HTTP Factory for the requests the proper way) because this was supposed to be a very basic module of code.

I've tried to create rich domain models, and didn't went overhead with getting validation done in a more generic way. I don't think it was needed as base example.

The way to check the functionality is to go to the bottom of the page. For now, the EE github API URL works (as of the date of this push) but I can probably replace it in the future with my own static json docs (saved in API JSONs folder for this commit).

If you have questions, feel free to contact me.

# Instructions for the assignment
1. Clone this repository on your machine.
1. Use your IDE of choice to complete the assignment.
1. When you are finished with the solution and have pushed it to the repo, [you can submit the assignment here](https://app.snapcode.review/submission_links/6eb1b437-71c3-4806-a714-2dcd0e9ca446).

# About the task

We’ll be using a simple shopping cart, similar to those used by e-commerce websites, as the domain for this problem.

There’s no time limit for this task. We expect that you can give a representative piece of code in 1-2 hours

## Tips on what we’re looking for

### 1. Test coverage

Your solution should:

- be developed 'test-first'
- have good unit tests
- cover common paths
- be self-contained and not rely on external systems when running tests. Use fakes/ mocks/ stubs as you see fit.
- in a "real" project integration tests may use a network could run over the network but this is *not required* for this assessment.

### 2. Simplicity

We value simplicity as an architectural virtue and a development practice. Solutions should reflect the difficulty of the assigned task, and shouldn’t be overly complex. We prefer simple, well tested solutions over clever solutions.

Please avoid:
- A web, desktop, command line or any other kind of app
- Unnecessary layers of abstraction
- Unnecessary patterns/ architectural features that aren’t called for

### 3. Self-explanatory code

The solution you produce must speak for itself. Multiple paragraphs explaining the solution is a sign that the code isn’t straightforward enough to understand on its own.

### 4. Dealing with ambiguity

If there’s any ambiguity, please add this in a section at the bottom of the README. You should also make a choice to resolve the ambiguity.

# Begin the task

Create a shopping cart ***package*** that facilitates 2 basic capabilities.

1. Add a product to the cart
   1. Specifying the product name and quantity
   2. Use the product name to discover the price from the [Product API](#product-api) specified below
   3. Cart state (totals, etc.) must be available

2. Calculate the state:
   1. Cart subtotal (sum of price for all items)
   2. Tax payable (charged at 12.5% on the subtotal)
   3. Total payable (subtotal + tax)
   4. Totals should be rounded up where required

## Product API

Base URL: `https://equalexperts.github.io/`

View Product: `GET /backend-take-home-test-data/{product}.json`

Available products
* `cheerios`
* `cornflakes`
* `frosties`
* `shreddies`
* `weetabix`

## Example
The below is a sample with the correct values you can use to confirm your calculations

### Inputs
* Add 1 × cornflakes @ 2.52 each
* Add another 1 x cornflakes @2.52 each
* Add 1 × weetabix @ 9.98 each
  
### Results  
* Cart contains 2 x cornflakes
* Cart contains 1 x weetabix
* Subtotal = 15.02
* Tax = 1.88
* Total = 16.90

### How to validate the Scenario
* Navigate into EqualExpertsShoppingCartIntegrationTests/Services/CartServiceIntegrationTests.cs
* Search for the test AddProductToCart_EEScenario_AddsProductToCartAndPrintsState()
* Check the output to see if it matches the Results section just above this