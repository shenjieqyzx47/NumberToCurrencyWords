# Running this application 

## Prerequisites
Before running the application, make sure you have the following installed:

.NET 10 SDK  
Node.js

### 1. Clone the repository

```shell
git clone https://github.com/shenjieqyzx47/NumberToWords.git  
cd NumberToWords\NumberToWords
```

### 2. Run backend

```shell
dotnet restore
dotnet run
```
Do not close this terminal once it is running.

### 3. Run UI

Create a new terminal under same folder (NumberToWords\NumberToWord)
```shell
cd Web\convert-number-to-words
npm install
npm run dev
```

### 4. Test in UI
open http://localhost:5173/ in a browser and start testing.
Input anything in the input box and hit the convert button.

# Feature

This is an application featuring a web server routine that accepts a numerical input, converts the
number into words, and returns the result as a string output parameter.

### Implementation Plan ###

This feature is implemented using a TDD approach, as the requirements are clear.  
By using TDD, the expected scenarios are defined before implementation.   
Tests are written first to describe the desired behaviour, followed by the minimum code required to make those tests pass.   
Once the tests pass, the implementation is refactored to improve readability and maintainability without changing its behaviour.

### Test plan

Unit tests using Xunit will be created to test the core logic of converting numerical input to English words.  
Tests should cover following scenarios:

1. Non-numerical input should return error message
2. Long range numerical input should return warning message
3. Valid input numbers should cover all lenght range numbers including negative and decimals.

### The implementation plan

1. create tests
2. implement logic
3. expose endpoint
4. create UI interface
5. documentation

## Summary and conclusion

This section explains my whole implementation experience and the decision makings.  
The source control included all the approaches and attempts I have made.  
When the requirement comes, some of the acceptence criteras are pretty clear:

1. input must be a number.
2. input number can be recongaisied as money.
3. the application must have a web interface.
4. the logic must be well tested.
5. documentations.

And plus there are some extra considerations maybe included as:
1. input number can be negative.
2. the length of input number.
3. plural unit display.

As the tests are required and the business logic is pretty static, I decided to use with test driven design.  
And I made the decision to use decimal to hold the numbers and the number can be negative.

### In my first approach:  
Considering the number has a length constraint, I listed all the unique words for numbers along with all possible scales in decimal range (thousand, million ... ).  
Then I decided to start with low lenght number, e.g a function that converts number less than 1000 to words.  
Then numbers less then million. Then billion ..  
And soon I realize this is not efficient as you need to determine how many functions need to be called by the lenght of the input numbers

### Then my second approach:  
I started to merge the common logic in those small functions that I create in approach 1.  
However, due to some limitations, it did not work.

### And with my 3rd approach:  
I started to rethink the way to implement the logic.  
I destruct the input numbers into a 3-digit groups like how they are written daily (e.g 1,321,000)  
And each group gets its own convert result and eventually combine them together.

With the 3rd approach works, I started to make some improvements on the logic, where it supports plural unit display to the users,  
and some edge case handling (e.g 100001 converts into "ten thousands and one" instead of "ten thousands, one").


I had pretty good experience implementing this application. This involves business logic analysis, decision making, documentaion.  
I feel there must be better algrithm for this solution, happy to discuss.