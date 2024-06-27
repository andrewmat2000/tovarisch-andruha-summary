<script lang="ts">
  import Answer from "@components/codewars/Answer.svelte";
  import Test from "@components/codewars/Test.svelte";
  import Task from "@components/codewars/Task.svelte";
  import { validatePIN } from "@lib/codewars/pin-validation/answer";
  import code from "@lib/codewars/pin-validation/answer.js?raw";

  const test = {
    func: validatePIN,
    cases: [
      { args: "1", expected: false },
      { args: "12", expected: false },
      { args: "123", expected: false },
      { args: "12345", expected: false },
      { args: "1234567", expected: false },
      { args: "-1234", expected: false },
      { args: "-1.234", expected: false },
      { args: "00000000", expected: false },
      { args: "a234", expected: false },
      { args: ".234", expected: false },
      { args: "1234", expected: true },
      { args: "0000", expected: true },
      { args: "1111", expected: true },
      { args: "123456", expected: true },
      { args: "098765", expected: true },
      { args: "000000", expected: true },
      { args: "123456", expected: true },
      { args: "090909", expected: true },
    ],
  };

  const title = "Regex validate PIN code";
  const text = `ATM machines allow 4 or 6 digit PIN codes and PIN codes cannot contain anything but exactly 4 digits or exactly 6 digits.

If the function is passed a valid PIN string, return true, else return false.`;
  const example = `"1234"   -->  true
"12345"  -->  false
"a234"   -->  false`;
</script>

<div>
  <Task {example} {text} {title} />
</div>
<div>
  <Answer {code}></Answer>

  <Test {test}></Test>
</div>
