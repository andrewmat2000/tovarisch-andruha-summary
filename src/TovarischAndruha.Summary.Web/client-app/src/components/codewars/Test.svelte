<script lang="ts" generics="R, A">
  export let test: { func: (args: A) => R; cases: { expected: R; args: A }[] };

  function getFormatedInput(test: { expected: R; args: A }) {
    switch (typeof test.args) {
      case "string":
        return `'${test.args}'`;
      case "number":
      case "bigint":
      case "boolean":

      case "undefined":
      case "function":
        return `${test.args}`;

      case "object":
        return JSON.stringify(test.args);
    }

    return test.args;
  }
</script>

{#each test.cases as testCase}
  <div>
    {test.func.name}({getFormatedInput(testCase)}) =
    {#if test.func(testCase.args) == testCase.expected}
      <span class={"success"}>
        {test.func(testCase.args)}
      </span>
    {:else}
      <span class={"fail"}>
        {test.func(testCase.args)}
      </span>
      <span>({testCase.expected})</span>
    {/if}
  </div>
{/each}

<style>
  .success {
    color: green;
  }
  .fail {
    color: red;
  }
</style>
