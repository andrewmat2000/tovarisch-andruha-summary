<div>
  <slot />
</div>

<style lang="scss">
  @import url("https://unpkg.com/svelte-highlight/styles/github.css");

  :global(.hljs) {
    background-color: transparent;

    color: $theme-text-color;
  }

  :global(
      .hljs-string,
      .hljs-literal,
      .hljs-variable,
      .hljs-regexp,
      .hljs-number
    ) {
    color: #2d7cd6;
  }
  
  :global(.hljs-title.function_) {
    color: #8951f1;
  }

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
