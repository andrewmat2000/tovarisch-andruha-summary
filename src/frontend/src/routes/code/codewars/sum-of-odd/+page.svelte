<script lang="ts">
  import Answer from "@components/codewars/Answer.svelte";
  import Test from "@components/codewars/Test.svelte";
  import Task from "@components/codewars/Task.svelte";
  import { rowSumOddNumbers } from "@lib/codewars/sum-of-odd/answer";
  import code from "@lib/codewars/sum-of-odd/answer.js?raw";

  const test = {
    func: rowSumOddNumbers,
    cases: [
      { args: 1, expected: 1 },
      { args: 42, expected: 74088 },
    ],
  };

  const title = "Sum of odd numbers";
  const text = `Given the triangle of consecutive odd numbers:

             1
          3     5
       7     9    11
   13    15    17    19
21    23    25    27    29
...

Calculate the sum of the numbers in the nth row of this triangle (starting at index 1) e.g.: (Input --> Output)`;
  const example = `1 --> 1
2 --> 3 + 5 = 8`;
</script>

<div>
  <Task {example} {text} {title} />
</div>
<div>
  <Answer {code}></Answer>

  <Test {test}></Test>
</div>
