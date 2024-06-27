<script lang="ts">
  import { _ } from "svelte-i18n";
  import { uiStore } from "@lib/stores/ui-store";
  import Link from "./Link.svelte";

  function solveUrl(url: string) {
    const split = url.split("/").filter((x) => x.length > 0);

    let array: {
      href: string;
      label: string;
    }[] = [];

    let currentUrl = "/";

    for (let urlPart of split) {
      const labelToFind = `bread-crumbs-${urlPart}`;

      currentUrl += urlPart;

      array.push({
        href: currentUrl,
        label:
          $_(labelToFind) == labelToFind
            ? labelToFind.slice(13)
            : $_(labelToFind),
      });

      if (split[split.length - 1] != urlPart) {
        currentUrl += "/";
      }
    }

    return array;
  }
</script>

<div>
  {#each solveUrl($uiStore.url) as url}
    <Link label={url.label} link={url.href} type="a" />
    {#if url.href != $uiStore.url}
      <span>/</span>
    {/if}
  {/each}
  {#if $uiStore.url.length <= 1}
    <Link label={$_("bread-crumbs-main")} link="/" type="a" />
  {/if}
</div>

<style lang="scss">
  div {
    display: flex;
    height: 100%;

    align-items: center;
  }

  span {
    font-size: 12px;
    padding: 0 3px;
    color: $theme-text-color;
    opacity: 0.6;
  }
</style>
