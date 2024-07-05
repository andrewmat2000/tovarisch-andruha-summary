<script lang="ts">
  import { uiStore } from "@lib/stores/ui-store";

  export let link: string;
  export let label: string;
  export let callback: () => void = () => {};

  export let type: "button" | "a" = "button";

  function go() {
    uiStore.goto(link);

    callback();
  }
</script>

<input type="button" on:click={go} value={label} class={type} />

<style lang="scss">
  input {
    border: none;
    background-color: transparent;
    color: $theme-text-color;
  }

  input:hover {
    cursor: pointer;
  }

  .button {
    border-radius: 3px;
    height: 35px;

    width: 100%;
  }
  .button:hover {
    background-color: $theme-white-color;
  }

  .a:hover {
    cursor: pointer;
    opacity: 0.5;
  }
</style>
