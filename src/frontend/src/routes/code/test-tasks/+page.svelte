<script lang="ts">
  import Link from "@components/Link.svelte";
  import { t } from "svelte-i18n";
  const tasks = [
    {
      label: $t("promit-test-task"),
      link: "/code/test-tasks/promit",
    },
  ];
</script>

<div>
  {#each tasks as task}
    <Link label={task.label} link={task.link} />
  {/each}
</div>
