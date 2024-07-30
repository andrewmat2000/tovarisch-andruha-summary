<script lang="ts">
  import { uiStore } from "@lib/stores/ui-store";
</script>

{#if $uiStore.isAuthorized}
  <slot />
{:else}
  <h1>NOT FOUND</h1>
{/if}
