<div>
  <slot />
</div>

<style lang="scss">
  div {
    display: grid;
    grid-template-columns: 1fr 1fr;
    height: 100%;
    width: 100%;

    > :global(*) {
      padding: 15px;
    }

    > :global(*:first-child) {
      border-right: 1px solid black;
    }
  }
</style>
