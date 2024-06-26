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
    {
      link: "/code/codewars/pin-validate",
      label: $_("pin-validate-link-label"),
    },
    {
      link: "/code/codewars/sum-of-odd",
      label: $_("sum-of-odd-link-label"),
    },
  ];
</script>

<div>
  {#each tasks as task}
    <Link label={task.label} link={task.link} />
  {/each}
</div>

<style lang="scss">
  div {
    overflow-y: auto;
  }
</style>
