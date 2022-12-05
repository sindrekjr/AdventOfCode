pub fn solve(part: i32, input: String) -> String {
    match part {
        1 => solve_part_one(input),
        2 => solve_part_two(input),
        _ => String::new(),
    }
}

pub fn solve_part_one(input: String) -> String {
    let paragraphs = input.split("\n\n").collect::<Vec<_>>();
    let mut stacks = parse_stacks(paragraphs[0]);

    for line in paragraphs[1].lines() {
        let mv: Vec<usize> = line
            .split_whitespace()
            .filter_map(|w| match w.parse::<usize>() {
                Ok(n) => Some(n),
                Err(_) => None,
            })
            .collect();

        let count = mv[0];
        let old_stack = &mut stacks[mv[1] - 1];
        let items = old_stack.split_off(old_stack.len() - count);
        let new_stack = &mut stacks[mv[2] - 1];
        for item in items.iter().rev() {
            new_stack.push(item.to_owned());
        }
    }

    stacks
        .into_iter()
        .map(|stack| stack.last().unwrap().to_owned())
        .collect()
}

pub fn solve_part_two(input: String) -> String {
    let paragraphs = input.split("\n\n").collect::<Vec<_>>();
    let mut stacks = parse_stacks(paragraphs[0]);

    for line in paragraphs[1].lines() {
        let mv: Vec<usize> = line
            .split_whitespace()
            .filter_map(|w| match w.parse::<usize>() {
                Ok(n) => Some(n),
                Err(_) => None,
            })
            .collect();

        let count = mv[0];
        let old_stack = &mut stacks[mv[1] - 1];
        let items = old_stack.split_off(old_stack.len() - count);
        let new_stack = &mut stacks[mv[2] - 1];
        for item in items {
            new_stack.push(item);
        }
    }

    stacks
        .into_iter()
        .map(|stack| stack.last().unwrap().to_owned())
        .collect()
}

fn parse_stacks(crates: &str) -> Vec<Vec<char>> {
    let mut first = true;
    let mut stacks = Vec::new();
    for line in crates.lines().rev() {
        if first {
            first = false;
            for _ in line.chars().enumerate().filter(|&(i, _)| i % 4 == 1) {
                stacks.push(Vec::new());
            }
        } else {
            let chars: Vec<char> = line
                .chars()
                .enumerate()
                .filter(|&(i, _)| i % 4 == 1)
                .map(|(_, v)| v)
                .collect();

            for (i, c) in chars.into_iter().enumerate() {
                if !c.is_whitespace() {
                    stacks[i].push(c);
                }
            }
        }
    }

    stacks
}
