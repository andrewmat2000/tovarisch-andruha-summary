<script lang="ts">
  import { uiStore } from "@lib/stores/ui-store";
  import { isMobile, type LinkType } from "@lib";

  export let link: LinkType;
  export let callback: (() => void) | undefined = undefined;

  export let type: "button" | "a" = "button";

  function go() {
    uiStore.goto(link.path);

    callback?.();
  }
</script>

<input type="button" on:click={go} value={link.label} class={`${type} ${isMobile() ? "mobile" : "desktop"}`} />

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

    border-bottom: $theme-white-color solid 1px;
  }

  .button:hover {
    background-color: $theme-white-color;
  }

  .a:hover {
    cursor: pointer;
    opacity: 0.5;
  }

  .mobile[class*="button"] {
    height: 55px;
  }
</style>
