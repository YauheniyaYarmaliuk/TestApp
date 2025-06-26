# TestApp
Test application for students C#

**The task**
We need you to do the following as part of this task:
1. Write a list of test cases to check that this new feature satisfies the acceptance criteria
   provided bellow. In this list you must:
   a. Describe some high-level steps and expectations (you can make assumptions of
   how the app works - just explain it at the beginning)
   b. Highlight what are the inputs you will be using on each test case
   c. Define testing level of these test cases: unit testing, component testing or e2e
   testing (manual) - considering that:
   i. We have a unit test framework in TestApp using Nunit framework
   ii. We have a component test framework in our TestAppAPI using Nunit
   framework
   iii. We don't have any automation to test the UI, so manual testing will be
   required on e2e level
   d. Indicate which test cases you would add to regression suite and which not
2. Write the code for all automated tests you described on the different frameworks
3. Write a SQL query that will return "all the StudyGroups which have at least one user with
   'name' starting on 'M' sorted by 'creation date'" like "Miguel" or "Manuel"
   There are multiple ways to approach this task, so as a first thing, explain us what approach you
   selected, the reasoning behind this decision and if you already had past experience implementing
   a similar approach or not

   **Acceptance criteria**
1. There can be only one Study Group for a single Subject
   a. The name for the Study Group with size between 5-30 characters
   b. The only valid Subjects are: Math, Chemistry, Physics
   c. We want to record when Study Groups were created
2. Users can join Study Groups for different Subjects
3. Users can check the list of all existing Study Groups
   a. Users can also filter Study Groups by a given Subject
   b. Users can sort to see most recently created Study Groups or oldest ones
4. Users can leave Study Groups they joined

