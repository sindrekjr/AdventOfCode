pub fn solve(part: i32, input: String) -> String {
    match part {
        1 => solve_part_one(input),
        2 => solve_part_two(input),
        _ => String::new(),
    }
}

pub fn solve_part_one(input: String) -> String {
    parse_calorie_totals(&input)
        .max()
        .unwrap()
        .to_string()
}

pub fn solve_part_two(input: String) -> String {
    let mut elves = parse_calorie_totals(&input)
        .collect::<Vec<u32>>();
    
    elves.sort();
    elves.reverse();

    elves.into_iter().take(3).sum::<u32>().to_string()
}

fn parse_calorie_totals<'a>(input: &'a str) -> impl Iterator<Item = u32> + 'a {
    input
        .split("\n\n")
        .map(|elf| elf.lines().filter_map(|c| c.parse::<u32>().ok()).sum())
}
