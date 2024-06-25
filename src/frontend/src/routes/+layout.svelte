<script lang="ts">
  import { waitLocale, register, init } from "svelte-i18n";

  register("ru", () => import("@lib/i18n/ru.json"));

  init({
    fallbackLocale: "ru",
    initialLocale: "ru",
  });
</script>

{#await waitLocale() then}
  <main>
    <slot />
  </main>
{/await}

<style>
  main {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
  }
</style>
