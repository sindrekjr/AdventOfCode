use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day19::solve_part_one(input),
        Part::P2 => Day19::solve_part_two(input),
    }
}

#[derive(Debug)]
struct Blueprint {
    id: u8,
    ore: u8,
    clay: u8,
    obsidian: (u8, u8),
    geode: (u8, u8),
}

impl From<&str> for Blueprint {
    fn from(inp: &str) -> Self {
        let mut split = inp.split("Each ").map(|design| design.split_whitespace());

        let id = split
            .next()
            .unwrap()
            .nth(1)
            .unwrap()
            .split(':')
            .next()
            .unwrap()
            .parse::<u8>()
            .unwrap();

        let ore = split.next().unwrap().nth(3).unwrap().parse::<u8>().unwrap();
        let clay = split.next().unwrap().nth(3).unwrap().parse::<u8>().unwrap();

        let obsidian = match split
            .next()
            .unwrap()
            .skip(3)
            .collect::<Vec<&str>>()
            .as_slice()
        {
            &[a, _, _, b, ..] => (a.parse::<u8>().unwrap(), b.parse::<u8>().unwrap()),
            _ => panic!(),
        };

        let geode = match split
            .next()
            .unwrap()
            .skip(3)
            .collect::<Vec<&str>>()
            .as_slice()
        {
            &[a, _, _, b, ..] => (a.parse::<u8>().unwrap(), b.parse::<u8>().unwrap()),
            _ => panic!(),
        };

        Self {
            id,
            ore,
            clay,
            obsidian,
            geode,
        }
    }
}

impl Blueprint {
    fn run(&self, minutes: u16) -> u16 {
        minutes * self.id as u16
    }
}

struct Day19;
impl Solution for Day19 {
    fn solve_part_one(input: String) -> Option<String> {
        let blueprints = input.lines().map(Blueprint::from);

        // blueprints
        //     .map(|blueprint| blueprint.run(24))
        //     .sum::<u16>()
        //     .to_string()

        String::new()
    }

    fn solve_part_two(_input: String) -> String {
        String::new()
    }
}
