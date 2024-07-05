<script lang="ts">
  import Answer from "@components/codewars/Answer.svelte";
  import Test from "@components/codewars/Test.svelte";
  import Task from "@components/codewars/Task.svelte";
  import { duplicateCount } from "@lib/codewars/counting-duplicates/answer";
  import code from "@lib/codewars/counting-duplicates/answer.js?raw";
  import { githubDark } from "svelte-highlight/styles";

  const test = {
    func: duplicateCount,
    cases: [
      { args: "", expected: 0 },
      { args: "abcde", expected: 0 },
      { args: "aabbcde", expected: 2 },
      { args: "aabBcde", expected: 2 },
      { args: "Indivisibility", expected: 1 },
      { args: "Indivisibilities", expected: 2 },
    ],
  };

  const title = "Counting Duplicates";
  const text = `Write a function that will return the count of distinct case-insensitive alphabetic characters and numeric digits that occur more than once in the input string. The input string can be assumed to contain only alphabets (both uppercase and lowercase) and numeric digits.`;
  const example = `"abcde" -> 0 # no characters repeats more than once
"aabbcde" -> 2 # 'a' and 'b'
"aabBcde" -> 2 # 'a' occurs twice and 'b' twice (\`b\` and \`B\`)
"indivisibility" -> 1 # 'i' occurs six times
"Indivisibilities" -> 2 # 'i' occurs seven times and 's' occurs twice
"aA11" -> 2 # 'a' and '1'
"ABBA" -> 2 # 'A' and 'B' each occur twice`;
</script>

<svelte:head>
  {@html githubDark}
</svelte:head>
<div>
  <Task {example} {text} {title} />
</div>
<div>
  <Answer {code}></Answer>
  <Test {test} />
</div>
