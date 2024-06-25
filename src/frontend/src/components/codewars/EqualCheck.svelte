<script lang="ts" generics="R, A">
  export let test: { expected: R; func: (args: A) => R; args: A };

  function getFormatedInput() {
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

<div>
  {test.func.name}({getFormatedInput()}) =
  <span class={test.func(test.args) == test.expected ? "success" : "fail"}>
    {test.func(test.args)}
  </span>
</div>

<style>
  .success {
    color: green;
  }
  .fail {
    color: red;
  }
</style>
