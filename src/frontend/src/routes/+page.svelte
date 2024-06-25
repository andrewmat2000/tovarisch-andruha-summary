<script lang="ts">
  import Link from "@components/Link.svelte";
  import { _ } from "svelte-i18n";
</script>

<Link label={$_("main-tasks-link-label")} link="/tasks" />
