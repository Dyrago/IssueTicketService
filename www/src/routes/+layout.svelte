<script lang="ts">
	import favicon from '$lib/assets/favicon.svg';

	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
	<link rel="../lib/styles/styles.css" type="stylesheet" />
</svelte:head>

<div class="container">
	<header>
		<h1><a href="/">IssueTicketService</a></h1>
	</header>

	{@render children()}

	<footer id="footer">
		Created by Dyrago
	</footer>
</div>

<style>
	*{
		margin: 0;
		padding: 0;
		box-sizing: border-box;
	}

	body
	{
		margin: 0;
		padding: 0;
		box-sizing: border-box;
	}

	header{
		margin-bottom: 1rem;
		border: 1px solid black;
		padding: 1rem;
		color: #fff;
		background-color: blue;
	}

	h1 > a {
		direction: none;
		text-decoration: none;
		color: #fff;
	}

	.container{
		display: flex;
		flex-direction: column;
		min-height: 100dvh;
	}

	footer{
		border: 1px solid black;
		padding: 1rem;
		margin-top: 1rem;
		background-color: #333;
		color: #fff;
		text-align: center;
	}
</style>