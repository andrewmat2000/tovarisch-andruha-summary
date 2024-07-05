<script lang="ts">
  import { githubDark } from "svelte-highlight/styles";
</script>

<svelte:head>
  <link
    rel="stylesheet"
    href="https://unpkg.com/svelte-highlight/styles/github.css"
  />
  {@html githubDark}
</svelte:head>

<div>
  <slot />
</div>

<style lang="scss">
  div {
    display: grid;
    grid-template-columns: 1fr 1fr;
    height: 100%;
    width: 100%;

    > :global(*:first-child) {
      border-right: 1px solid $theme-white-color;
    }

    > :global(*) {
      max-height: $theme-main-height;
      overflow: auto;
      padding: 0 15px;
    }
  }

  :global(span, h1, h2, div) {
    color: $theme-text-color;
  }
</style>
