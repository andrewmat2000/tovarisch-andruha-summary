<script lang="ts">
  import Link from "@components/Link.svelte";
  import type { LinkType } from "@lib";
  import { uiStore } from "@lib/stores/ui-store";
  import { _ } from "svelte-i18n";

  const links: LinkType[] = [
    {
      label: $_("admin-panel-link-users"),
      path: uiStore.getNextUrl("/users"),
    },
    {
      label: $_("admin-panel-link-cms"),
      path: uiStore.getNextUrl("/cms"),
    },
  ];
</script>

{#each links as link}
  <Link {link} />
{/each}

<style>
</style>
