<script lang="ts" generics="R, A">
  export let test: { func: (args: A) => R; cases: { expected: R; args: A }[] };


  function formatData(value: R | A) {
    if (Array.isArray(value)) {
      return `[${value}]`;
    }

    switch (typeof value) {
      case "string":
        return `'${value}'`;
      case "number":
      case "bigint":
      case "boolean":

      case "undefined":
      case "function":
        return `${value}`;

      case "object":
        return JSON.stringify(value);
    }

    return value;
  }

  const results: { actual: R; success: boolean; index: number }[] = [];

  for (let i = 0; i < test.cases.length; i++) {
    const testCase = test.cases[i];
    const actual = test.func(testCase.args);

    results.push({
      actual: actual,
      index: i,
      success: Array.isArray(testCase.expected)
        ? JSON.stringify(actual) == JSON.stringify(testCase.expected)
        : actual == testCase.expected,
    });
  }
</script>

{#each results as result}
  <div>
    {test.func.name}({formatData(test.cases[result.index].args)}) =
    {#if result.success}
      <span class={"success"}>
        {formatData(result.actual)}
      </span>
    {:else}
      <span class={"fail"}>
        {formatData(result.actual)}
      </span>
      <span>({test.cases[result.index].expected})</span>
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
