<script lang="ts">
  import Link from "@components/Link.svelte";
  import { _ } from "svelte-i18n";

  const tasks: {
    label: string;
    link: string;
  }[] = [
    {
      link: "/code/codewars/counting-duplicates",
      label: $_("counting-duplicates-link-label"),
    },
  ];
</script>

<div>
  {#each tasks as task}
    <Link label={task.label} link={task.link} />
  {/each}
</div>
