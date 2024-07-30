<script lang="ts">
  import { uiStore, type User } from "@lib/stores/ui-store";
  import { onMount } from "svelte";

  async function loadProfile() {
    const response = await fetch("/api/users/get_profile");
    const json = (await response.json()) as User;

    uiStore.update(x => {
      x.user = json;

      x.isAuthorized = x.user.login != "guest";

      return x;
    });
  }

  onMount(async () => {
    await loadProfile();

    uiStore.init();
    uiStore.update(x => {
      x.loaded = true;
      return x;
    });
  });
</script>
