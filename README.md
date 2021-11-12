This repo contains two solutions for Reckon test

**DivisorConsoleApp.sln**

*	This is a .net core consle app that resolves first problem of divisor 
*	Can be opened in Visual Studio and be executed using F5 or Debug menu.
*	Solution is single layered only and demonstrates the logic used to generate output based on list of divisors.
*	It invokes the given reckon APIs with a certain number of retries and obtains range and divisor information.
*	It uses C# mod (%) operator to work out if given number in range is divisible by divisor.
*	The results are formatted and output to console.

**Possible Improvements:**

*	Convert to a layered application and separate core logic into a library.
*	Add unit tests to verify boundary conditions and core logic.
*	Use dependency injection to inject replaceable strategy for generting output.
*	There are some basic input validations - add more.
*	Use generics to abstract different API calls.
*	Better injection and clean-up of HttpClient.
*	Better configuration for retry attempt.

**TextSearchApi.sln**

*	This is a .net core consle app that resolves the second test which requires to write REST api for text search.
*	Can be opened in Visual Studio and be executed using F5 or Debug menu.  It will be opened in default browser using IIS express.
*	The api GET /api/textsearch contains the results desired. It also posts to given endpoint.
*	Solution is single layered only and demonstrates the logic used to search subtext in given string without using any C# string functions.
*	Solution also formats and outputs result.
*	It internally uses a helper utility to compare subtext char by char within given content.

**Possible Improvements:**

*	Convert to a layered application and separate core logic into a library.
*	Add unit tests to verify boundary conditions and core logic.
*	Use dependency injection to inject replaceable strategy for search logic.
*	There are some basic input validations - add more.
*	Use generics to abstract different API calls.
*	Better injection and clean-up of HttpClient.
*	Better configuration for retry attempt.
