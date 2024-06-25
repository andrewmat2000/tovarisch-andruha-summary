<script lang="ts">
  import Link from "@components/Link.svelte";
  import { _ } from "svelte-i18n";

  const tasks: {
    label: string;
    link: string;
  }[] = [
    {
      link: "/tasks/codewars/counting-duplicates",
      label: $_("counting-duplicates-link-label"),
    },
  ];
</script>

{#each tasks as task}
  <Link label={task.label} link={task.link} />
{/each}
