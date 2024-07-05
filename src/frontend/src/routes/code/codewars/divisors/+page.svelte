<script lang="ts">
  import Task from "@components/codewars/Task.svelte";
  import Answer from "@components/codewars/Answer.svelte";
  import Test from "@components/codewars/Test.svelte";

  import { divisors } from "@lib/codewars/divisors/answer";
  import code from "@lib/codewars/divisors/answer.js?raw";

  const title = "Find the divisors! ";
  const text = `Create a function named divisors/Divisors that takes an integer n > 1 and returns an array with all of the integer's divisors(except for 1 and the number itself), from smallest to largest. If the number is prime return the string '(integer) is prime' (null in C#, empty table in COBOL) (use Either String a in Haskell and Result<Vec<u32>, String> in Rust).`;
  const example = `divisors(12); // should return [2,3,4,6]
divisors(25); // should return [5]
divisors(13); // should return "13 is prime"
`;

  const test = {
    func: divisors,
    cases: [
      {
        expected: [3, 5],
        args: 15,
      },
      {
        expected: [2, 3, 4, 6],
        args: 12,
      },
      {
        expected: "13 is prime",
        args: 13,
      },
    ],
  };
</script>

<div>
  <Task {example} {text} {title} />
</div>
<div>
  <Answer {code}></Answer>

  <Test {test}></Test>
</div>
