<script lang="ts">
  import { githubDark } from "svelte-highlight/styles";
  import { isMobile } from "@lib";
</script>

<svelte:head>
  <link
    rel="stylesheet"
    href="https://unpkg.com/svelte-highlight/styles/github.css"
  />
  {@html githubDark}
</svelte:head>

<div class={isMobile() ? "mobile" : "desktop"}>
  <slot />
</div>

<style lang="scss">
  :global(span, h1, h2, div) {
    color: $theme-text-color;
  }

  .desktop {
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
    }

    > :global(*:first-child) {
      padding-right: 15px;
    }
    > :global(*:last-child) {
      padding-left: 15px;
    }
  }

</style>
