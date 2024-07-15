<script lang="ts">
  import Link from "@components/Link.svelte";
  import { _ } from "svelte-i18n";

  const projects: {
    link: string;
    label: string;
  }[] = [
    {
        link: "/code/my-projects/urfu-notification-system",
        label: $_("urfu-notification-system-link-label"),
    }
  ];
</script>

<div>
  {#each projects as project}
    <Link label={project.label} link={project.link} />
  {/each}
</div>
